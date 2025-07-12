using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MOS.Domain.Entities;

namespace MOS.Data.EF.Access.Contexts.Extensions;

public static class AuditableEntityExtensions
{
    public static void ApplyAuditableConfig<TKey>(
        this IMutableEntityType entityType,
        ModelBuilder modelBuilder)
        where TKey : struct
    {
        var builder = modelBuilder.Entity(entityType.ClrType);
        
        builder
            .Property("CreatedAt")
            .IsRequired()
            .HasDefaultValueSql("NOW()");
        
        builder
            .Property("CreatedBy")
            .IsRequired()
            .HasDefaultValue(1);
                
        builder
            .Property("UpdatedBy")
            .IsRequired(false)
            .HasDefaultValue(null);
                
        builder
            .Property("DeletedBy")
            .IsRequired(false)
            .HasDefaultValue(null);
                
        builder
            .HasOne("Creator")
            .WithMany()
            .HasForeignKey("CreatedBy")
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne("Updater")
            .WithMany()
            .HasForeignKey("UpdatedBy")
            .OnDelete(DeleteBehavior.Restrict);
                
        builder.HasOne("Deleter")
            .WithMany()
            .HasForeignKey("DeletedBy")
            .OnDelete(DeleteBehavior.Restrict);
        
        if (typeof(IAuditableEntity<TKey>).IsAssignableFrom(entityType.ClrType))
        {
            var filter = GetSoftDeleteFilter(entityType.ClrType);
            builder.HasQueryFilter(filter);
            var foundProperty = entityType.FindProperty(nameof(IAuditableEntity<TKey>.IsDeleted));
            if (foundProperty != null)
            {
                entityType.AddIndex(foundProperty);
            }
        }
    }
    
    private static LambdaExpression GetSoftDeleteFilter(Type entityType)
    {
        var method = typeof(AuditableEntityExtensions)
            .GetMethod(nameof(CreateFilter), BindingFlags.NonPublic | BindingFlags.Static);
        return (LambdaExpression)method.MakeGenericMethod(entityType).Invoke(null, null);
    }
    
    private static Expression<Func<TEntity, bool>> CreateFilter<TEntity>() 
        where TEntity : IAuditableEntity<int>
        => e => !e.IsDeleted;
}