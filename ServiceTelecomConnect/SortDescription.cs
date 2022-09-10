using System.ComponentModel;

namespace ServiceTelecomConnect
{
    internal class SortDescription
    {
        private string v;
        private ListSortDirection descending;

        public SortDescription(string v, ListSortDirection descending)
        {
            this.v = v;
            this.descending = descending;
        }
    }
}