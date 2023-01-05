namespace StreamProof;

public class GroupManager
{
    public static List<Group> Groups = new();

    // Create a list of users
    public static List<User> Users = new()
    {
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Alice Smith", Group = "Group 1" },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Bob Johnson", Group = "Group 1" },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Charlie Williams", Group = "Group 2" },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Dave Jones", Group = "Group 2" },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Eve Brown", Group = "Group 3" },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "Frank Davis", Group = "Group 3" },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "Grace Miller", Group = "Group 4" },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "Heidi Wilson", Group = "Group 4" },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "Igor Moore", Group = "Group 5" },
        new() { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = "Jasmine Taylor", Group = "Group 5" }
    };

    static GroupManager()
    {
        // Add the users to the appropriate group's list of users
        foreach (var user in Users)
        {
            var group = Groups.Find(g => g.Name == user.Group);
            if (group == null)
            {
                group = new Group { Name = user.Group, Users = new List<User>() };
                Groups.Add(group);
            }
            group.Users.Add(user);
        }
    }
}

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Group { get; set; }
}

// Define the Group class with a name and list of users properties
public class Group
{
    public string Name { get; set; }
    public List<User> Users { get; set; }
}