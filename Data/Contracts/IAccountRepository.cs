﻿using Domain.Model;
using System;
using System.Collections.Generic;

namespace Data.Contracts
{
    public interface IAccountRepository
    {
        List<Account> GetAccountsByDate(DateTime date);
        List<Account> GetAccountsByMonth(int month, int year);
        List<Account> GetAccountsByYear(int year);
        Account GetAccountById(int accountId);
        bool AddAccount(Account account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(int accountId);
        bool AddNoteToAccount(int accountId, string note);
    }
}