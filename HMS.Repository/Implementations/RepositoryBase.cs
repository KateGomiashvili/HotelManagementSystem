﻿using HMS.Repository.Data;
using HMS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Repository.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        public RepositoryBase(ApplicationDbContext context)
        {
            _dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, int pageNumber, int pageSize, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query
                        .Include(includeProperty)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);
                }
                return await query.ToListAsync();
            }

            var entities = await query
                .Where(filter)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return entities;
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(int pageNumber, int pageSize, string includeProperties = null)
        {
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                IQueryable<T> query = _dbSet;

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                return await
                    query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            }

            return await
                _dbSet
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            var entity = await query.FirstOrDefaultAsync(filter);
            return entity;
        }

        public void Remove(T entity) => _dbSet.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
    }
}
