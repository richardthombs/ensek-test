
## Backend
The API backend is is split into four projects:

`EnsekTest` contains the classes and interfaces that make up the problem domain.

`EnsekTest.Implementation` contains implementation for CSV parsing and meter reading uploading.

`EnsekTest.Integrations.EntityFramework` is a persistence layer built using EF Core and Sqlite.

`EnsekTest.WebApi` contains the API itself.

Tests for CSV parsing and the uploading service are included in `EnsekTest.Implementation.Tests`.

## Frontend
The frontend is built with `Next.js`. It uses a single page and a set of components to render a simple uploading page. Styling is provided with `tailwindcss`.


## Building and deploying
There is a GitHub Actions workflow which automates test, build and deployment into a Kubernetes cluster. It is a bit basic because it create ":latest" tagged containers rather than appending a build id, and the deployment step just deletes the existing Kubernetes deployment and re-creates it rather than patching the existing one.


## Implementation notes
1. **The API is not authorised in any way whatsoever!**
1. I've assumed that a duplicate reading is one where the account, date _and_ value match.
1. The Sqlite database is created within the container, so it will get destroyed whenever the container is restarted. Not fit for production!
1. The API returns the number of successful and failed readings and also an annotated list of all the readings so that the frontend can display which lines were successful and which ones failed. I know this is more than asked for, hope I don't get penalized for not following the spec.
