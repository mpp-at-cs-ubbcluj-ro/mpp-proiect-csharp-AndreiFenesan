namespace persistance.repositoryInterfaces;

using model;
public interface IVolunteerRepository : IRepository<long, Volunteer>
{
    Volunteer? FindVolunteerByUsername(string username);
}