using System.ComponentModel;

namespace Vonage.Conversations.GetConversations;

/// <summary>
///     Defines the ordering when fetching data.
/// </summary>
public enum FetchOrder
{
    /// <summary>
    ///     The ascending order.
    /// </summary>
    [Description("asc")] Ascending,

    /// <summary>
    ///     The descending order.
    /// </summary>
    [Description("desc")] Descending,
}