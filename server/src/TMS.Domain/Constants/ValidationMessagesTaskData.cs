namespace TMS.Domain.Constants
{
    public class ValidationMessagesTaskData
    {
        public const string Error_Name_NoEmpity = "Name is required";
        public const string Error_Name_Length = "Name needs to be between 2 and 200 characters";

        public const string Error_StartDate_LessThan = "The Start Date should be less than the End Date";

        public const string Error_FinishDate_Null = "The Finish Date should not have value if the task is not started";
        public const string Error_FinishDate_GreaterThan = "The Finish Date should be greater than the Start Date";
    }
}
