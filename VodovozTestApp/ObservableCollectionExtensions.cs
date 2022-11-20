using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VodovozTestApp;

public static class ObservableCollectionExtensions
{
    public static void ClearAndAddRange<T>(this ObservableCollection<T> source, IEnumerable<T> newItems)
    {
        source.Clear();

        foreach(var item in newItems)
        {
            source.Add(item);
        }
    }
}
