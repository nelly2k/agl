# This project is a test project to apply for Snr. Software Developer @ AGL.

## Notes
There are three projects in the solution:

### App
All business logic goes here (fetching, grouping, orchestrating)

### Test
- Tests for business loggic
- Integration test for fetcher
:grey_question: I didn't create any tests for pipes, think it is a bit excessive

### UI
Console app to represent a result

## Assumptions
- If there aren't any cats for certain gender, it won't appear in a list

## Non functional
I put connection string into config, however in a real live I wouldn't do it due to security reasons (don't want my configs in source control). 




