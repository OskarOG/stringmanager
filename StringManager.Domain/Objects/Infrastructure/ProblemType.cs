namespace StringManager.Domain.Objects.Infrastructure;

public enum ProblemType
{
    Unknown = 0,
    InvalidEmail,
    InvalidObjectName,
    InvalidNewPassword,
    UnableToParseUserRoleType,
    EmptyOrNullFolderDescription,
    NoUserFound,
    IncorrectPassword
}