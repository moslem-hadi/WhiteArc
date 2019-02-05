using System;
using System.Collections.Generic;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DomainClasses
{

     


    #region Category
    public class Category
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public int SortOrder { get; set; }
        public string Text { get; set; }
    }

    public class CategoryNode : Category
    {
        public CategoryNode(Category category)
        {
            Id = category.Id;
            ParentCategoryId = category.ParentCategoryId;
            SortOrder = category.SortOrder;
            Text = category.Text;
        }

        public CategoryTree Children { get; set; }
        public int Level { get; set; }
        public string DisplayText
        {
            get
            {
                return string.Concat(new string('—', Level ), Text);
            }
        }
    }

    public class CategoryTree : IEnumerable<CategoryNode>
    {
        private List<CategoryNode> innerList = new List<CategoryNode>();

        public CategoryTree(IEnumerable<CategoryNode> nodes)
        {
            innerList = new List<CategoryNode>(nodes);
        }

        public IEnumerator<CategoryNode> GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerable<CategoryNode> Flatten()
        {
            foreach (var category in innerList.OrderBy(o => o.SortOrder))
            {
                yield return category;
                if (category.Children != null)
                {
                    foreach (var child in category.Children.Flatten())
                    {
                        yield return child;
                    }
                }
            }
        }

        public static CategoryTree Create(IEnumerable<Category> categories, Func<Category, bool> parentPredicate, int level = 0)
        {
            var nodes = categories
                .Where(parentPredicate)
                .OrderBy(o => o.SortOrder)
                .Select(item =>
                    new CategoryNode(item)
                    {
                        Level = level,
                        Children = Create(categories, o => o.ParentCategoryId == item.Id, level + 1)
                    });

            return new CategoryTree(nodes);
        }
    }
    #endregion


    public static class CacheConfig
    {
        public static int Duration = 3;//second
    }

    /// <summary>
    /// برای قابل تغییر بودن زمان کش همه اکشن ها از یکجا، این کلاس نوشته شده
    /// </summary>
    public class CustomOutputCacheAttribute : OutputCacheAttribute
    {
        public CustomOutputCacheAttribute()
        {
            this.Duration = CacheConfig.Duration;
        }
    }

}
