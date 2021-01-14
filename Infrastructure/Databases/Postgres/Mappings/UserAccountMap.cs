using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Postgres.Mappings
{
    internal class UserAccountMap : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("user_account");

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
                .HasMaxLength(64);

            builder.Property(x => x.InitialBalance)
                .IsRequired();
            builder.Property(x => x.CurrentBalance)
                .IsRequired();

            builder.OwnsOne(x => x.Cpf)
                .Property(x => x.Number)
                .HasColumnName("CpfNumber")
                .HasMaxLength(11)
                .IsRequired();

            builder.OwnsOne(x => x.Password)
                .Property(x => x.Hash)
                .HasColumnName("PasswordHash")
                .HasMaxLength(64)
                .IsRequired();

            builder.OwnsOne(x => x.Password)
                .Property(x => x.Salt)
                .HasColumnName("PasswordSalt")
                .HasMaxLength(8)
                .IsRequired();

            builder.OwnsOne(x => x.Name)
                .Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(100)
                .IsRequired();

            builder.OwnsOne(x => x.Name)
                .Property(x => x.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
