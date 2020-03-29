

public interface IEntity
{
   string EntityName { get; set; }
   string EntityInfo { get; set; }
   float EntityValue { get; set; }
   

   void UpdateEntityInfo();
}
