using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sapp.Common
{
    public class Issuetype
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("iconUrl")] public string IconUrl { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("subtask")] public bool Subtask { get; set; }

        [JsonProperty("avatarId")] public int AvatarId { get; set; }
    }

    public class StatusCategory
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("key")] public string Key { get; set; }

        [JsonProperty("colorName")] public string ColorName { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }

    public class Status
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("iconUrl")] public string IconUrl { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("statusCategory")] public StatusCategory StatusCategory { get; set; }
    }

    public class Priority
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("iconUrl")] public string IconUrl { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("id")] public string Id { get; set; }
    }

    public class Fields
    {
        [JsonProperty("summary")] public string Summary { get; set; }

        [JsonProperty("status")] public Status Status { get; set; }

        [JsonProperty("priority")] public Priority Priority { get; set; }

        [JsonProperty("issuetype")] public Issuetype Issuetype { get; set; }

        [JsonProperty("statuscategorychangedate")]
        public DateTime Statuscategorychangedate { get; set; }

        [JsonProperty("parent")] public Parent Parent { get; set; }

        [JsonProperty("timespent")] public object Timespent { get; set; }

        [JsonProperty("project")] public Project Project { get; set; }

        [JsonProperty("fixVersions")] public List<FixVersion> FixVersions { get; set; }

        [JsonProperty("aggregatetimespent")] public object Aggregatetimespent { get; set; }

        [JsonProperty("resolution")] public object Resolution { get; set; }

        [JsonProperty("customfield_10940")] public object Customfield10940 { get; set; }

        [JsonProperty("customfield_10500")] public List<Customfield10500> Customfield10500 { get; set; }

        [JsonProperty("customfield_10941")] public Customfield10941 Customfield10941 { get; set; }

        [JsonProperty("customfield_10501")] public Customfield10501 Customfield10501 { get; set; }

        [JsonProperty("customfield_10700")] public string Customfield10700 { get; set; }

        [JsonProperty("customfield_10503")] public object Customfield10503 { get; set; }

        [JsonProperty("customfield_10702")] public object Customfield10702 { get; set; }

        [JsonProperty("customfield_10900")] public string Customfield10900 { get; set; }

        [JsonProperty("customfield_10901")] public object Customfield10901 { get; set; }

        [JsonProperty("customfield_10902")] public object Customfield10902 { get; set; }

        [JsonProperty("resolutiondate")] public object Resolutiondate { get; set; }

        [JsonProperty("customfield_10506")] public object Customfield10506 { get; set; }

        [JsonProperty("customfield_10507")] public object Customfield10507 { get; set; }

        [JsonProperty("customfield_10903")] public object Customfield10903 { get; set; }

        [JsonProperty("customfield_10904")] public object Customfield10904 { get; set; }

        [JsonProperty("customfield_10905")] public object Customfield10905 { get; set; }

        [JsonProperty("workratio")] public int Workratio { get; set; }

        [JsonProperty("customfield_10906")] public object Customfield10906 { get; set; }

        [JsonProperty("customfield_10907")] public object Customfield10907 { get; set; }

        [JsonProperty("customfield_10908")] public object Customfield10908 { get; set; }

        [JsonProperty("customfield_10909")] public object Customfield10909 { get; set; }

        [JsonProperty("lastViewed")] public DateTime? LastViewed { get; set; }

        [JsonProperty("watches")] public Watches Watches { get; set; }

        [JsonProperty("created")] public DateTime Created { get; set; }

        [JsonProperty("customfield_10021")] public double? Customfield10021 { get; set; }

        [JsonProperty("customfield_10300")] public Customfield10300 Customfield10300 { get; set; }

        [JsonProperty("labels")] public List<string> Labels { get; set; }

        [JsonProperty("customfield_10301")] public object Customfield10301 { get; set; }

        [JsonProperty("customfield_10016")] public List<Customfield10016> Customfield10016 { get; set; }

        [JsonProperty("customfield_10930")] public object Customfield10930 { get; set; }

        [JsonProperty("customfield_10017")] public string Customfield10017 { get; set; }

        [JsonProperty("timeestimate")] public object Timeestimate { get; set; }

        [JsonProperty("aggregatetimeoriginalestimate")]
        public object Aggregatetimeoriginalestimate { get; set; }

        [JsonProperty("customfield_10935")] public Customfield10935 Customfield10935 { get; set; }

        [JsonProperty("issuelinks")] public List<Issuelink> Issuelinks { get; set; }

        [JsonProperty("assignee")] public Assignee Assignee { get; set; }

        [JsonProperty("updated")] public DateTime Updated { get; set; }

        [JsonProperty("components")] public List<Component> Components { get; set; }

        [JsonProperty("timeoriginalestimate")] public object Timeoriginalestimate { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("customfield_10015")] public string Customfield10015 { get; set; }

        [JsonProperty("customfield_10600")] public object Customfield10600 { get; set; }

        [JsonProperty("security")] public object Security { get; set; }

        [JsonProperty("customfield_10800")] public object Customfield10800 { get; set; }

        [JsonProperty("aggregatetimeestimate")]
        public object Aggregatetimeestimate { get; set; }

        [JsonProperty("customfield_10924")] public object Customfield10924 { get; set; }

        [JsonProperty("customfield_10929")] public object Customfield10929 { get; set; }

        [JsonProperty("creator")] public Creator Creator { get; set; }

        [JsonProperty("subtasks")] public List<object> Subtasks { get; set; }

        [JsonProperty("reporter")] public Reporter Reporter { get; set; }

        [JsonProperty("customfield_10000")] public DateTime? Customfield10000 { get; set; }

        [JsonProperty("aggregateprogress")] public Aggregateprogress Aggregateprogress { get; set; }

        [JsonProperty("customfield_10001")] public object Customfield10001 { get; set; }

        [JsonProperty("customfield_10200")] public Customfield10200 Customfield10200 { get; set; }

        [JsonProperty("customfield_10201")] public object Customfield10201 { get; set; }

        [JsonProperty("environment")] public object Environment { get; set; }

        [JsonProperty("customfield_10910")] public object Customfield10910 { get; set; }

        [JsonProperty("customfield_10911")] public object Customfield10911 { get; set; }

        [JsonProperty("customfield_10912")] public object Customfield10912 { get; set; }

        [JsonProperty("customfield_10913")] public object Customfield10913 { get; set; }

        [JsonProperty("duedate")] public object Duedate { get; set; }

        [JsonProperty("customfield_10914")] public Customfield10914 Customfield10914 { get; set; }

        [JsonProperty("customfield_10915")] public object Customfield10915 { get; set; }

        [JsonProperty("progress")] public ProgressDto ProgressDto { get; set; }

        [JsonProperty("customfield_10916")] public object Customfield10916 { get; set; }

        [JsonProperty("votes")] public VotesDto VotesDto { get; set; }
    }

    public class Parent
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("key")] public string Key { get; set; }

        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("fields")] public Fields Fields { get; set; }
    }

    public class AvatarUrls
    {
        [JsonProperty("48x48")] public string _48x48 { get; set; }

        [JsonProperty("24x24")] public string _24x24 { get; set; }

        [JsonProperty("16x16")] public string _16x16 { get; set; }

        [JsonProperty("32x32")] public string _32x32 { get; set; }
    }

    public class ProjectCategory
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }

    public class Project
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("key")] public string Key { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("projectTypeKey")] public string ProjectTypeKey { get; set; }

        [JsonProperty("simplified")] public bool Simplified { get; set; }

        [JsonProperty("avatarUrls")] public AvatarUrls AvatarUrls { get; set; }

        [JsonProperty("projectCategory")] public ProjectCategory ProjectCategory { get; set; }
    }

    public class FixVersion
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("archived")] public bool Archived { get; set; }

        [JsonProperty("released")] public bool Released { get; set; }
    }

    public class Customfield10500
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("value")] public string Value { get; set; }

        [JsonProperty("id")] public string Id { get; set; }
    }

    public class Customfield10941
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("value")] public string Value { get; set; }

        [JsonProperty("id")] public string Id { get; set; }
    }

    public class Customfield10501
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("value")] public string Value { get; set; }

        [JsonProperty("id")] public string Id { get; set; }
    }

    public class Watches
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("watchCount")] public int WatchCount { get; set; }

        [JsonProperty("isWatching")] public bool IsWatching { get; set; }
    }

    public class Customfield10300
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("value")] public string Value { get; set; }

        [JsonProperty("id")] public string Id { get; set; }
    }

    public class Customfield10016
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("boardId")] public int BoardId { get; set; }

        [JsonProperty("goal")] public string Goal { get; set; }

        [JsonProperty("startDate")] public DateTime StartDate { get; set; }

        [JsonProperty("endDate")] public DateTime EndDate { get; set; }

        [JsonProperty("completeDate")] public DateTime CompleteDate { get; set; }
    }

    public class Customfield10935
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("value")] public string Value { get; set; }

        [JsonProperty("id")] public string Id { get; set; }
    }

    public class Type
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("inward")] public string Inward { get; set; }

        [JsonProperty("outward")] public string Outward { get; set; }

        [JsonProperty("self")] public string Self { get; set; }
    }

    public class OutwardIssue
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("key")] public string Key { get; set; }

        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("fields")] public Fields Fields { get; set; }
    }

    public class InwardIssue
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("key")] public string Key { get; set; }

        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("fields")] public Fields Fields { get; set; }
    }

    public class Issuelink
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("type")] public Type Type { get; set; }

        [JsonProperty("outwardIssue")] public OutwardIssue OutwardIssue { get; set; }

        [JsonProperty("inwardIssue")] public InwardIssue InwardIssue { get; set; }
    }

    public class Assignee
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("accountId")] public string AccountId { get; set; }

        [JsonProperty("emailAddress")] public string EmailAddress { get; set; }

        [JsonProperty("avatarUrls")] public AvatarUrls AvatarUrls { get; set; }

        [JsonProperty("displayName")] public string DisplayName { get; set; }

        [JsonProperty("active")] public bool Active { get; set; }

        [JsonProperty("timeZone")] public string TimeZone { get; set; }

        [JsonProperty("accountType")] public string AccountType { get; set; }
    }

    public class Component
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
    }

    public class Creator
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("accountId")] public string AccountId { get; set; }

        [JsonProperty("emailAddress")] public string EmailAddress { get; set; }

        [JsonProperty("avatarUrls")] public AvatarUrls AvatarUrls { get; set; }

        [JsonProperty("displayName")] public string DisplayName { get; set; }

        [JsonProperty("active")] public bool Active { get; set; }

        [JsonProperty("timeZone")] public string TimeZone { get; set; }

        [JsonProperty("accountType")] public string AccountType { get; set; }
    }

    public class Reporter
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("accountId")] public string AccountId { get; set; }

        [JsonProperty("emailAddress")] public string EmailAddress { get; set; }

        [JsonProperty("avatarUrls")] public AvatarUrls AvatarUrls { get; set; }

        [JsonProperty("displayName")] public string DisplayName { get; set; }

        [JsonProperty("active")] public bool Active { get; set; }

        [JsonProperty("timeZone")] public string TimeZone { get; set; }

        [JsonProperty("accountType")] public string AccountType { get; set; }
    }

    public class Aggregateprogress
    {
        [JsonProperty("progress")] public int Progress { get; set; }

        [JsonProperty("total")] public int Total { get; set; }
    }

    public class Customfield10200
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("value")] public string Value { get; set; }

        [JsonProperty("id")] public string Id { get; set; }
    }

    public class NonEditableReason
    {
        [JsonProperty("reason")] public string Reason { get; set; }

        [JsonProperty("message")] public string Message { get; set; }
    }

    public class Customfield10914
    {
        [JsonProperty("hasEpicLinkFieldDependency")]
        public bool HasEpicLinkFieldDependency { get; set; }

        [JsonProperty("showField")] public bool ShowField { get; set; }

        [JsonProperty("nonEditableReason")] public NonEditableReason NonEditableReason { get; set; }
    }

    public class ProgressDto
    {
        [JsonProperty("progress")] public int Progress { get; set; }

        [JsonProperty("total")] public int Total { get; set; }
    }

    public class VotesDto
    {
        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("votes")] public int Votes { get; set; }

        [JsonProperty("hasVoted")] public bool HasVoted { get; set; }
    }

    public class Issue
    {
        [JsonProperty("expand")] public string Expand { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("self")] public string Self { get; set; }

        [JsonProperty("key")] public string Key { get; set; }

        [JsonProperty("fields")] public Fields Fields { get; set; }
    }

    public class JiraSearchDto
    {
        [JsonProperty("expand")] public string Expand { get; set; }

        [JsonProperty("startAt")] public int StartAt { get; set; }

        [JsonProperty("maxResults")] public int MaxResults { get; set; }

        [JsonProperty("total")] public int Total { get; set; }

        [JsonProperty("issues")] public List<Issue> Issues { get; set; }
    }
}