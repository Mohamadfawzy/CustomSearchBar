using System;
using System.Collections.Generic;
using System.Text;

namespace CustomSearchBar.Services
{

    public static class SearchDataStore
    {
        private static List<string> DataRecentSearch = new List<string>()
        {
            "pizza",
            "mixed grill",
            "Full English breakfast",
            "sausage and mash",
            "Green Soup", // ملوخية
            "Fried potatoes",// بطاطس مقلية
            "Lentils", // عدس
            "Macaroni", //  مكرونة
            "shrimp", // جمبري
            "Roast beef", // لحم مشوى | لحم بقرى مشوى
            "Hawawshi",
            "Cow's trotters", // كوارع
            "Sheep intestines", // بمبار
            "Fish Broth",  // مرقة سمل
            "Fish Fillet", //  سمك فيلية
            "Lentils", // عدس
            "Macaroni", //  مكرونة
            "shrimp", // جمبري

        };
        public static IEnumerable<string> GetItems()
        {
            return DataRecentSearch;
        }
        public static void SetItem(string item)
        {
            DataRecentSearch.Add(item);
        }
    }


}
