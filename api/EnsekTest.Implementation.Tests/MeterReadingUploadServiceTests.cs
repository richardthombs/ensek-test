using System;
using System.Collections.Generic;

using NUnit.Framework;
using FakeItEasy;

namespace EnsekTest.Implementation.Tests
{
	public class MeterReadingUploadServiceTests
	{
		readonly MeterReading DummyReading = new MeterReading
		{
			AccountId = 1,
			MeterReadingDateTime = DateTime.Today,
			MeterReadValue = 99
		};

		[Test]
		public void Duplicate_Readings_Are_Not_Uploaded()
		{
			// Arrange
			var meterRepo = A.Fake<IMeterReadingsRepository>();
			A.CallTo(() => meterRepo.Exists(A<MeterReading>.Ignored)).Returns(true);
			var accountsRepo = A.Fake<IAccountsRepository>();
			A.CallTo(() => accountsRepo.Exists(A<int>.Ignored)).Returns(true);

			// Act
			var uploadSvc = new MeterReadingUploadService(meterRepo, accountsRepo);
			uploadSvc.Upload(new List<SubmittedMeterReading>()
			{
				new SubmittedMeterReading
				{
					Valid = true,
					ParsedReading = DummyReading
				}
			});

			// Assert
			A.CallTo(() => meterRepo.Exists(A<MeterReading>.That.Matches(x =>
				x.AccountId == 1 &&
				x.MeterReadingDateTime == DateTime.Today &&
				x.MeterReadValue == 99
			))).MustHaveHappened();
			A.CallTo(() => meterRepo.Create(A<MeterReading>.Ignored)).MustNotHaveHappened();
		}

		[Test]
		public void Invalid_Readings_Are_Not_Uploaded()
		{
			// Arrange
			var meterRepo = A.Fake<IMeterReadingsRepository>();
			var accountsRepo = A.Fake<IAccountsRepository>();
			A.CallTo(() => accountsRepo.Exists(A<int>.Ignored)).Returns(true);

			// Act
			var uploadSvc = new MeterReadingUploadService(meterRepo, accountsRepo);
			uploadSvc.Upload(new List<SubmittedMeterReading>()
			{
				new SubmittedMeterReading
				{
					Valid = false,
					ParsedReading = DummyReading
				}
			});

			// Assert
			A.CallTo(() => meterRepo.Create(A<MeterReading>.Ignored)).MustNotHaveHappened();
		}

		[Test]
		public void Valid_Readings_Are_Uploaded()
		{
			// Arrange
			var meterRepo = A.Fake<IMeterReadingsRepository>();
			var accountsRepo = A.Fake<IAccountsRepository>();
			A.CallTo(() => accountsRepo.Exists(A<int>.Ignored)).Returns(true);

			// Act
			var uploadSvc = new MeterReadingUploadService(meterRepo, accountsRepo);
			var uploaded = uploadSvc.Upload(new List<SubmittedMeterReading>()
			{
				new SubmittedMeterReading
				{
					Valid = true,
					ParsedReading = DummyReading
				}
			});

			// Assert
			A.CallTo(() => meterRepo.Create(A<MeterReading>.That.Matches(x =>
				x.AccountId == 1 &&
				x.MeterReadingDateTime == DateTime.Today &&
				x.MeterReadValue == 99
			))).MustHaveHappened();
			Assert.AreEqual(true, uploaded[0].Uploaded);
		}

		[Test]
		public void Invalid_Accounts_Are_Not_Uploaded()
		{
			// Arrange
			var meterRepo = A.Fake<IMeterReadingsRepository>();
			var accountRepo = A.Fake<IAccountsRepository>();
			A.CallTo(() => accountRepo.Exists(A<int>.Ignored)).Returns(false);

			// Act
			var uploadSvc = new MeterReadingUploadService(meterRepo, accountRepo);
			var uploaded = uploadSvc.Upload(new List<SubmittedMeterReading>()
			{
				new SubmittedMeterReading
				{
					Valid = true,
					ParsedReading = DummyReading
				}
			});

			// Assert
			A.CallTo(() => meterRepo.Create(A<MeterReading>.Ignored)).MustNotHaveHappened();
			Assert.AreEqual(false, uploaded[0].Uploaded);
			Assert.AreEqual(false, uploaded[0].Valid);
		}
	}
}
