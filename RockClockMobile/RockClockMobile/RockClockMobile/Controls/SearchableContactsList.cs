using Xamarin.Forms.Internals;
using RockClockMobile.Models.Navigation;
using RockClockMobile.Models;

namespace RockClockMobile.Controls
{
    /// <summary>
    /// This class extends the behavior of the SearchableListView control to filter the ListViewItem based on text.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SearchableContactsList : SearchableListView
    {
        #region Method

        /// <summary>
        /// Filtering the list view items based on the search text.
        /// </summary>
        /// <param name="obj">The list view item</param>
        /// <returns>Returns the filtered item</returns>
        public override bool FilterContacts(object obj)
        {
            if (base.FilterContacts(obj))
            {
                var taskInfo = obj as RocksUser;
                if (taskInfo == null || string.IsNullOrEmpty(taskInfo.firstName))
                {
                    return false;
                }

                return taskInfo.firstName.ToUpperInvariant().Contains(this.SearchText.ToUpperInvariant());
            }

            return false;
        }

        #endregion
    }
}