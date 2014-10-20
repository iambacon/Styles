namespace IAmBacon.Framework.Mvc
{
    /// <summary>
    /// The URL helper.
    /// </summary>
    public interface IUrlHelper
    {
        /// <summary>
        /// Generates a fully qualified URL to an action method by using the specified action name and route values.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="routeValues">An object that contains the parameters for a route.</param>
        /// <returns></returns>
        string Action(string actionName, object routeValues);
    }
}
