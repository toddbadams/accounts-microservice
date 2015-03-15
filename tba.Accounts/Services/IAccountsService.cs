using System;
using tba.accounts.Models;

namespace tba.accounts.Services
{
    public interface IAccountsService : IDisposable
    {
        /// <summary>
        /// Fetch an array of accounts for a given parent entity
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="parentId">OPTIONAL - an account parent entity</param>
        /// <returns>array of account read-only models</returns>
        AccountRm[] Fetch(long tenantId, long userId, long? parentId=null);

        /// <summary>
        /// Insert a new account
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="account"></param>
        /// <returns>a read-only account model</returns>
        AccountRm Insert(long tenantId, long userId, AccountCm account);

        /// <summary>
        /// Update an existing account
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="accountId">the id of the account to update</param>
        /// <param name="account">the account update model</param>
        /// <returns>a read-only account model</returns>
        AccountRm Update(long tenantId, long userId, long accountId, AccountUm account);

        /// <summary>
        /// Mark and existing account as open (not complete)
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="accountId">the id of the account to mark as open</param>
        /// <returns>a read-only account model</returns>
        AccountRm CloseAccount(long tenantId, long userId, long accountId);

        /// <summary>
        /// Delete an existing account
        /// </summary>
        /// <param name="tenantId">a tenant</param>
        /// <param name="userId">a user</param>
        /// <param name="id">the id of the account to delete</param>
        void Delete(long tenantId, long userId, long id);
    }
}