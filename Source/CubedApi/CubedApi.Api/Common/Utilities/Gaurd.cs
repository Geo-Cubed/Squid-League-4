namespace CubedApi.Api.Common.Utilities
{
    public static class Gaurd
    {
        /// <summary>
        /// Checks if an object is null or not
        /// </summary>
        /// <param name="item">The object to check</param>
        /// <returns>True if null, false otherwise</returns>
        public static bool IsNull(this object item)
        {
            if (item == null)
            {
                return true;
            }

            return false;
        }
    }
}
