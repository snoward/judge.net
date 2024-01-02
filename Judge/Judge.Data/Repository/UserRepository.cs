﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Judge.Model;
using Judge.Model.Account;
using Judge.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Judge.Data.Repository;

internal sealed class UserRepository : IUserRepository
{
    private readonly DataContext context;

    public UserRepository(DataContext context)
    {
        this.context = context;
    }

    public User? Get(long id)
    {
        return this.BaseQuery().FirstOrDefault(o => o!.Id == id);
    }

    public void Add(User user)
    {
        this.context.Set<User>().Add(user);
    }

    public void Update(User user)
    {
        this.context.Entry(user).State = EntityState.Modified;
    }

    public void Delete(User user)
    {
        this.context.Set<User>().Remove(user);
    }

    public User? FindByName(string userName)
    {
        return this.BaseQuery().FirstOrDefault(o => o!.UserName == userName);
    }

    public User? FindByEmail(string email)
    {
        return this.BaseQuery().FirstOrDefault(o => o!.Email == email);
    }

    public Task<User?> FindByEmailAsync(string email)
    {
        return this.BaseQuery().FirstOrDefaultAsync(o => o!.Email == email);
    }

    public IEnumerable<User> Find(ISpecification<User> specification)
    {
        return this.BaseQuery().Where(specification.IsSatisfiedBy!)!;
    }

    public async Task<IReadOnlyCollection<User>> SearchAsync(ISpecification<User> specification)
    {
        return (await this.BaseQuery().Where(specification.IsSatisfiedBy!).ToListAsync())!;
    }

    public Task<User?> GetAsync(long id)
    {
        return this.BaseQuery().FirstOrDefaultAsync(o => o!.Id == id);
    }

    private IIncludableQueryable<User?, ICollection<UserRole>> BaseQuery()
    {
        return this.context.Set<User?>().Include(o => o!.UserRoles);
    }
}