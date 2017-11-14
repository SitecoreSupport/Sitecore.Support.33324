namespace Sitecore.Support.Publishing.Service.Events
{
  using Sitecore.Data;
  using Sitecore.Data.Eventing.Remote;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Framework.Publishing.Manifest;

  public class RemoteItemEventFactory : Sitecore.Publishing.Service.Events.RemoteItemEventFactory
  {
    protected override SavedItemRemoteEvent CreateSavedEvent(ItemResult.IItemModifiedResult resultData, Sitecore.Data.Items.Item savedItem)
    {
      var itemChanges = new ItemChanges(savedItem);

      if (resultData.ModifiedProperties.ChangeType != ResultChangeType.NotModified)
      {
        if (resultData.ModifiedProperties.IsMasterChanged())
        {
          var currentID = new ID(resultData.ModifiedProperties.Current.MasterId.Value);
          var previousId = ID.Null;

          if (resultData.ModifiedProperties.Previous != null)
          {
            previousId = new ID(resultData.ModifiedProperties.Previous.MasterId.Value);
          }

          itemChanges.SetPropertyValue("branchid", currentID, previousId);
        }

        if (resultData.ModifiedProperties.IsMoved())
        {
          var currentID = new ID(resultData.ModifiedProperties.Current.ParentId.Value);
          var previousId = ID.Null;

          if (resultData.ModifiedProperties.Previous != null)
          {
            previousId = new ID(resultData.ModifiedProperties.Previous.ParentId.Value);
          }

          itemChanges.SetPropertyValue("parentid", currentID, previousId);
        }

        if (resultData.ModifiedProperties.IsTemplateChanged())
        {
          var currentId = new ID(resultData.ModifiedProperties.Current.TemplateId);
          var previousId = ID.Null;

          if (resultData.ModifiedProperties.Previous != null)
          {
            previousId = new ID(resultData.ModifiedProperties.Previous.TemplateId);
          }

          itemChanges.SetPropertyValue("templateid", currentId, previousId);
        }

        if (resultData.ModifiedProperties.IsRenamed())
        {
          var previousName = string.Empty;

          if (resultData.ModifiedProperties.Previous != null)
          {
            previousName = resultData.ModifiedProperties.Previous.Name;
          }

          itemChanges.SetPropertyValue("name", resultData.ModifiedProperties.Current.Name, previousName);
        }

        foreach (var field in resultData.ModifiedFields)
        {
          if (field.Value != field.OriginalValue)
          {
            itemChanges.SetFieldValue(new Field(new ID(field.FieldId), savedItem), field.Value, field.OriginalValue);
          }
        }
      }

      return new SavedItemRemoteEvent(savedItem, itemChanges);
    }
  }
}