# LoginVsi Test Task

## Description

This is the test task implementation of the following test task:
>Tasks management WEB-API
Create a simple web api to create and read Users and Tasks.
Users are defined by unique Name
Tasks are defined by their Description and a State (Waiting, InProgress, Completed)
When a Task is created, an User should be assigned to it automatically .
Users can have multiple tasks, Task can only have one user.
Every 2 minutes all tasks should be reassigned to another random user (it can't be the user which is already assigned to the task)
When no users are available the Task will stay without assigned user.
All task have to be transferred for exactly 3 times, after that, they should be considered completed and stay unassigned.
No user interface is requirement for the assignment.
We purposely left requirements loose so candidate can do it as they like.

# Program Execution

Build with 
>dotnet build

>cd LoginvsiTestTask

> dotnet ef database update

>dotnet run
 
Open the browser at [http://localhost:PORT/swagger/index.html](http:localhost:PORT/swagger/index.html) to play with the solution. As an alternative, you can use any favorite IDE.
# Assumptions 
- It is a test task and all assumptions below are in the context of the test task, considering simplifications if possible. 
These assumptions are to be reviewed in the production code, and other decisions are to be made.
- No state transition management is defined for tasks, except the only transition to 'Completed' after 3 reassigning. 
Decided to make task state the input parameter to create the task(Completed is not accepted). Other possible 
solution - change the state with reassigning, however a new state(i.e. Created) is required to make 3 reassigning with state changes.
- Not expected to have many tasks/users, so no preliminary optimization. 
- Authentication/Authorization: Anonymous access  is used as no other requirements
- Database: SQLite database is used with no other requirements, and it does not have external dependencies
- Optimistic concurrency locking is used
- No premature optimizations are done
- Minimal logging is implemented
- REST-style Web API is used 
- No paging is implemented in GET requests
- Minimal error handling is used
- Configuration is hardcoded as constants
- Swagger is configured 
- Tests are not included 
- Default startup configuration with minor modifications is used 
- `Task` was renamed to `Assignment` to avoid conflicts with `System.Threading.Tasks.Task` 

# License

MIT
