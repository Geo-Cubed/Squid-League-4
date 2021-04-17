namespace CubedApi.Api.Common.Utilities
{
    public static class IdExtentions
    {
        /// <summary>
        /// Checks if a provided Id is <= 0
        /// </summary>
        /// <param name="Id">The interger id to check</param>
        /// <returns>True if the Id is invalid, false if not</returns>
        public static bool IsInvalid(this int Id)
        {
            if (Id <= 0)
            {
                return true;
            }

            return false;
        }
    }
}
