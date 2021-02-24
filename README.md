# CQSHelper

Helper for the CQS pattern for the .NET Core DI container. 

# NOTE
It's more efficient to use Autofac or SimpleInjector for dependency injection due to the way .NET core DI handles generic types.

# Setup
The setup includes copying the directory into your solution, registering the handlers to the DI container and that's it! Get going creating your commands and queries.
## Step 1
Copy the folder "CQSHelper" into your solution. 

## Step 2
Register handlers to .NET Core dependency injection service collection.
### startup.cs
Declarations:
   

     using CQSHelper;
ConfigureServices function:

  

    // CQS stuff
    services.UseCqsHelper(null, ServiceLifetime.Scoped);

The "RegisterCQS" extension method takes 2 param, the lifetime of the dependencies, and the assembly to collect the interface references from. defaults to the location of the CQSHelper library if left null. 

## Creating Queries
Query Example:

    public class UserByIdQuery : IQuery
    {
        public UserByIdQuery(string id) {
            this.Id = id;
        }

        public string Id { get; set; }
    }

    public class UserByIdQueryResult : IResult
    {
        public User User { get; set; }

    }

Query Handler Example:

    public class UserByIdQueryHandler : IQueryHandler<UserByIdQuery, UserByIdQueryResult>
    {
        private IUserRepository _userRepository;

        public UserByIdQueryHandler(IUserRepository userRepository) {
            _userRepository = userRepository;
        }


        public async Task<UserByIdQueryResult> ExecuteAsync(UserByIdQuery query)
        {
            var queryResult = new UserByIdQueryResult();
            queryResult.User = await _userRepository.GetUserById(query.Id);
            return queryResult;
        }
    }

## Creating Commands
Command example:

    public class AddUserCommand : ICommand
    {
        public string UserId{ get; set; }
        public string Name{ get; set; }
        public string Email{ get; set; }        
    }

Command Handler Example:

    public class AddUserCommandHandler : ICommandHandler<AddUserCommand>
    {
        private readonly IUserRepository _userRespository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRespository = userRepository;
        }

        public async override Task ExecuteAsync(AddUserCommand command)
        {
            var user = new User()
            {
                UserId = command.UserId,
                Name= command.Name,
                Email = command.Email
            };
            await _userRespository.AddUser(user);
        }
    }

## Dispatching from the controller
Query: 

    // GET: api/user/{id}
    [HttpGet("{id}")]
    public async Task<UserByIdQueryResult> GetById(string id) =>
        await _dispatcher.DispatchAsync<UserByIdQuery, UserByIdQueryResult>(new UserByIdQuery(id));

Command: 

    // POST: api/user/add
    [HttpPost("add")]
    public async Task AddUser(AddUserCommand command) =>
        await _dispatcher.DispatchAsync(command).ConfigureAwait(false);
