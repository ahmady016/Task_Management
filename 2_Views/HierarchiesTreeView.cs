using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Views;

public partial class HierarchiesTreeView
{
    public Guid DepartmentId { get; set; }
    public string DepartmentTitle { get; set; }
    public int NodeLevel { get; set; }
    public string TreeSequence { get; set; }
    public Guid? BossId { get; set; }
    public string BossTitle { get; set; }
}

public class HierarchiesTreeViewConfig : IEntityTypeConfiguration<HierarchiesTreeView>
{
    public void Configure(EntityTypeBuilder<HierarchiesTreeView> entity)
    {
        entity.ToView("hierarchies_tree_view");
        entity.HasNoKey();

        entity.Property(e => e.DepartmentId).HasColumnName("department_id");
        entity.Property(e => e.DepartmentTitle).HasColumnName("department_title");
        entity.Property(e => e.NodeLevel).HasColumnName("node_level");
        entity.Property(e => e.TreeSequence).HasColumnName("tree_sequence");
        entity.Property(e => e.BossId).HasColumnName("boss_id");
        entity.Property(e => e.BossTitle).HasColumnName("boss_title");
    }
}
