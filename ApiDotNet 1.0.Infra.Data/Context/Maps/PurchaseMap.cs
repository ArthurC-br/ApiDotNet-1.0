using ApiDotNet_1._0.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet_1._0.Infra.Data.Context.Maps
{
    public class PurchaseMap : IEntityTypeConfiguration<Purchase>

    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("compra");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idcompra")
                .UseIdentityColumn();
            builder.Property(x => x.PersonId)
                .HasColumnName("idpessoa");
            builder.Property(x => x.ProductId)
                .HasColumnName("idproduto");
            builder.Property(x => x.Date)
                .HasColumnType("date")
                .HasColumnName("datacompra");

            builder.HasOne(x => x.Person)
                .WithMany(x => x.Purchases);
            
            builder.HasOne(x => x.Product)
                .WithMany(x => x.Purchases);

        }
    }
}
