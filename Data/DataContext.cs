namespace discord_back_end.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<ChatGroup> ChatGroups { get; set; }
        public DbSet<ChatGroupMember> ChatGroupMembers { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<ServerMembership> ServerMemberships { get; set; }
        public DbSet<TextChannel> TextChannels { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasAlternateKey(u => u.Email);
            modelBuilder.Entity<User>().HasAlternateKey(u => u.UserName);

            modelBuilder.Entity<Friendship>().HasKey(uf => new { uf.UserId1, uf.UserId2 });
            modelBuilder.Entity<Friendship>().HasOne(f => f.User1).WithMany(u => u.Friendships).HasForeignKey(f => f.UserId1).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Friendship>().HasOne(f => f.User2).WithMany().HasForeignKey(f => f.UserId2).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ServerMembership>().HasKey(sm => new { sm.ServerId, sm.UserId });
            modelBuilder.Entity<ServerMembership>().HasOne(sm => sm.Server).WithMany(s => s.Members).HasForeignKey(sm => sm.ServerId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ServerMembership>().HasOne(sm => sm.User).WithMany(u => u.JoinedServers).HasForeignKey(sm => sm.UserId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChatGroupMember>().HasKey(cgm => new { cgm.ChatGroupId, cgm.UserId });
            modelBuilder.Entity<ChatGroupMember>().HasOne(cgm => cgm.ChatGroup).WithMany(cg => cg.Members).HasForeignKey(cgm => cgm.ChatGroupId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ChatGroupMember>().HasOne(cgm => cgm.User).WithMany(u => u.ChatGroups).HasForeignKey(cgm => cgm.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
