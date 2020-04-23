namespace Notion.DAL.Entity.Abstract
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}