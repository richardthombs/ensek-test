Implementation notes

1. **The API is not authorised in any way whatsoever!**
1. I assume that a duplicate reading is one where the account, date _and_ value match.
1. The uploading service works by creating a list of submitted meter readings and passing that list through parsing, validation and uploading. At the end, this list is then used to calculate the number of successful and rejected readings in the upload.


To build the container:
```
docker build . -t ensek-api:latest
```

To run the container:
```
docker run -it --rm -p 5000:5000  ensek-api:latest
```

To add an EF Core migration:
```
dotnet ef migrations add v001 -- Sqlite Filename=EnsekTest.db
```
