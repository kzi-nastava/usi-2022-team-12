﻿using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.GUI.Pagination.Requests;

namespace HealthInstitution.GUI.Pagination
{
    public static class PageExtensions
    {
        public static Page<TEntity> ToPage<TEntity>(this IEnumerable<TEntity> processedDbSet, PageRequest page) where TEntity : class
        {
            var totalElements = processedDbSet.Count();
            var totalPages = (int)Math.Ceiling((double)totalElements / page.Size);
            if (page.Page == -1)
            {
                page.Page = totalPages - 1;
            }
            var pageElements = processedDbSet.Skip(page.Page * page.Size).Take(page.Size).ToList();
            var count = pageElements.Count;

            return new Page<TEntity> { Entities = pageElements, TotalElements = totalElements, PageNumber = page.Page, Size = page.Size, PageCount = totalPages, Count = count };
        }
    }
}
