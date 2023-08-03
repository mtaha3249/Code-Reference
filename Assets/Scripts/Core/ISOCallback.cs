namespace Core
{
    /// <summary>
    /// Interface to handle all SO event callbacks
    /// </summary>
    public interface ISOCallback
    {
        /// <summary>
        /// Handle callback from raised Event
        /// </summary>
        /// <param name="param">parameters to fetch</param>
        public abstract void OnEventRaisedCallback(params object[] param);
    }
}