namespace Sitecore.Support.Publishing.Service.Pipelines.BulkPublishingEnd
{
  using Sitecore.Publishing.Service.Pipelines.BulkPublishingEnd;
  using Sitecore.Publishing.Service.SitecoreAbstractions;
  using Sitecore.Publishing.Service.Security;
  using Sitecore.Framework.Conditions;
  public class RaiseRemoteEvents : Sitecore.Publishing.Service.Pipelines.BulkPublishingEnd.RaiseRemoteEvents
  {
    public RaiseRemoteEvents(string remoteEventCacheClearingThreshold, ITargetCacheClearHistory targetCacheClearHistory) : base(
        new DatabaseFactoryWrapper(new PublishingLogWrapper()),
        new LanguageManagerWrapper(),
        new SitecoreSettingsWrapper(),
        new UserRoleService(),
        new PublishingLogWrapper(),
        new Sitecore.Support.Publishing.Service.Events.RemoteItemEventFactory(),
        targetCacheClearHistory,
        int.Parse(remoteEventCacheClearingThreshold))
    {
      Condition.Requires(remoteEventCacheClearingThreshold, "remoteEventCacheClearingThreshold").IsNotNull();
      Condition.Requires(targetCacheClearHistory, "targetCacheClearHistory").IsNotNull();
    }
  }
}