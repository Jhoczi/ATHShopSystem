﻿using System.Linq.Expressions;
using ath_server.Db;
using ath_server.Interfaces;
using ath_server.Services;
using Microsoft.EntityFrameworkCore;

namespace ath_server.Repositories;

public class RepositoryService<T> : IRepositoryService<T>
    where T : class, IEntity<int>
{
    protected DbContext _context;
    protected DbSet<T> _set;

    public RepositoryService(DataContext dbContext)
    {
        _context = dbContext;
        _set = dbContext.Set<T>();
    }

    public virtual ServiceResult Add(T entity)
    {
        ServiceResult result = new ServiceResult();
        try
        {
            _set.Add(entity);
            result = Save();
        }
        catch (Exception e)
        {
            result.Result = ServiceResultStatus.Error;
            result.Messages.Add(e.Message);
        }

        return result;
    }


    public virtual ServiceResult Delete(T entity)
    {
        ServiceResult result = new ServiceResult();
        try
        {
            _set.Remove(entity);
            result = Save();
        }
        catch (Exception e)
        {
            result.Result = ServiceResultStatus.Error;
            result.Messages.Add(e.Message);
        }

        return result;
    }


    public virtual ServiceResult Edit(T entity)
    {
        ServiceResult result = new ServiceResult();
        try
        {
            _context.Entry(entity).State = EntityState.Modified;

            result = Save();
        }
        catch (Exception e)
        {
            result.Result = ServiceResultStatus.Error;
            result.Messages.Add(e.Message);
        }

        return result;
    }

    public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
    {
        IQueryable<T> query = _set.Where(predicate);
        return query;
    }

    public IQueryable<T> GetAllRecords()
    {
        return _set;
    }

    public virtual T GetSingle(int id)
    {
        var result = _set.FirstOrDefault(r => r.Id == id);


        return result;
        ;
    }

    public virtual ServiceResult Save()
    {
        ServiceResult result = new ServiceResult();
        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            result.Result = ServiceResultStatus.Error;
            result.Messages.Add(e.Message);
        }

        return result;
    }
}