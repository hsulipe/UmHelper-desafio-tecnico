using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.SQLServer.Mappings
{
    internal class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transaction");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value)
                .IsRequired();
            builder.Property(x => x.From)
                .IsRequired();
            builder.Property(x => x.To)
                .IsRequired();

            builder.HasIndex(x => x.From);
            builder.HasIndex(x => x.To);

            builder.Property(x => x.Id)
                .HasMaxLength(64);

            builder.Property(x => x.From)
                .HasMaxLength(64);

            builder.Property(x => x.To)
                .HasMaxLength(64);

            // Foreign
            builder.HasOne<UserAccount>(x => x.UserAccountFrom)
                .WithMany(u => u.Sent)
                .HasForeignKey(x => x.From);

            builder.HasOne<UserAccount>(x => x.UserAccountTo)
                .WithMany(u => u.Received)
                .HasForeignKey(x => x.To);
        }
    }
}
