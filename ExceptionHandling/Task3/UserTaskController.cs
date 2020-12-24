using Task3.CustomExceptions;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskController
    {
        private readonly IUserTaskService _taskService;

        public UserTaskController(IUserTaskService taskService)
        {
            _taskService = taskService;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            string message = GetMessageForModel(userId, description);
            if (message != null)
            {
                model.AddAttribute("action_result", message);
                return false;
            }

            return true;
        }

        private string GetMessageForModel(int userId, string description)
        {
            var task = new UserTask(description);
            try
            {
                _taskService.AddTaskForUser(userId, task);
            }  
            catch (InvalidUserIdException)
            {
                return "Invalid userId";
            }
            catch (UserNotFoundException)
            {
                return "User not found";
            }
            catch (TaskAlreadyExistsException)
            {
                return "The task already exists";
            }

            return null;
        }
    }
}