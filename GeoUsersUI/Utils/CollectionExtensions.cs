using GeoUsers.Services.Model.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GeoUsersUI.Utils
{
    public static class CollectionExtensions
    {
        public static Collection<T> Update<T>(this Collection<T> source,
                                              IEnumerable<T> newCollection) where T : IBaseIdentifier
        {
            if (source.Any())
            {
                if (!newCollection.Any())
                {
                    source.Clear();

                    return source;
                }

                var itemsToAdd = newCollection.Except(source);

                foreach (var item in itemsToAdd)
                {
                    source.Add(item);
                }

                var itemsToUpdate = source.Intersect(newCollection);

                foreach (var item in itemsToUpdate)
                {
                    var newItem = newCollection.First(x => x.Id == item.Id);

                    item.Update(newItem);
                }

                var itemsToRemove = source.Except(newCollection).ToList();

                foreach (var item in itemsToRemove)
                {
                    source.Remove(item);
                }
            }
            else
            {
                foreach (var item in newCollection)
                {
                    source.Add(item);
                }
            }

            return source;
        }
    }
}
