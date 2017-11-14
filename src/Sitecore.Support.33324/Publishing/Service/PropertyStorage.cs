namespace Sitecore.Support.Publishing.Service
{
  using Sitecore.Data.Eventing.Remote;
  using System.Reflection;
  public class PropertyStorage
  {
    public static readonly PropertyInfo IsUnversionedFieldChanged = typeof(SavedItemRemoteEvent).GetProperty("IsUnversionedFieldChanged");
  }
}