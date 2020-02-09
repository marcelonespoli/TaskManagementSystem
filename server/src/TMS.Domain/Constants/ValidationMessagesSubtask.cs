namespace TMS.Domain.Constants
{
    public class ValidationMessagesSubtask
    {
        public const string Error_Name_NotEmpty = "Name is required";
        public const string Error_Name_Length = "Name needs to be between 2 and 250 characters";

        public const string Error_TaskId_NullOrEmpty = "Subtask must be associated with a task";
    }
}
