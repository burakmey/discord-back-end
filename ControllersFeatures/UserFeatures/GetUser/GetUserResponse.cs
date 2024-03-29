﻿namespace discord_back_end.ControllersFeatures.UserFeatures.GetUser
{
    public class GetUserResponse(User user)
    {
        public string Email { get; set; } = user.Email;
        public string UserName { get; set; } = user.UserName;
        public string DisplayName { get; set; } = user.DisplayName;
        public DateTime RegisteredAt { get; set; } = user.RegisteredAt;
        public ICollection<Server>? OwnedServers { get; set; } = user.OwnedServers;
        public ICollection<Friendship>? Friendships { get; set; } = user.Friendships;
        public ICollection<ChatGroupMember>? ChatGroups { get; set; } = user.ChatGroups;
        public ICollection<ServerMembership>? JoinedServers { get; set; } = user.JoinedServers;
    }
}
