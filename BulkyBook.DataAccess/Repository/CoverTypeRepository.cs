﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext Db) : base(Db)
        {
            _db = Db;
        }

        public void Update(CoverType coverType)
        {
            var objFromDb = _db.CoverTypes.FirstOrDefault(c => c.Id == coverType.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = coverType.Name;
            }


        }
    }
}
