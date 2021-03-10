Implementation notes

1. **The API is not authorised in any way whatsoever!**
1. I assume that a duplicate reading is one where the account, date _and_ value match.
1. The uploading service works by creating a list of submitted meter readings and passing that list through parsing, validation and uploading. At the end, this list is then used to calculate the number of successful and rejected readings in the upload.
