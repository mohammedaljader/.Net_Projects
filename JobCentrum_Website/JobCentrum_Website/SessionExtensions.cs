using JobCentrum_Website.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobCentrum_Website
{
    public static class SessionExtensions
    {
        public static void CreateJobSeeker(this ISession session)
        {
            if (!ContainsObject(session, "loggedInUser"))
            {
                JobSeekerAccountViewModel jobSeeker = new JobSeekerAccountViewModel
                {
                    Id = 0,
                    Fullname = "",
                    Username = "",
                    Password = "",
                    Email = "",
                    Telephone = "",
                    Address = "",
                    Seeker_Id = 0,
                    Experience = 0,
                    Hobby = ""
                };
                SetObject(session, "loggedInJobSeeker", jobSeeker);
            }
        }
        public static void CreateJobPublisher(this ISession session)
        {
            if (!ContainsObject(session, "loggedInUser"))
            {
                CompanyAccountViewModel JobPublisher = new CompanyAccountViewModel
                {
                    Id = 0,
                    Fullname = "",
                    Username = "",
                    Password = "",
                    Email = "",
                    Telephone = "",
                    Address = "",
                    Company_Id = 0,
                    Company_Name = "",
                    Company_Address = ""
                };
                SetObject(session, "loggedInUser", JobPublisher);
            }
        }
        public static void ResetJobSeeker(this ISession session)
        {
            if (ContainsObject(session, "loggedInJobSeeker"))
            {
                session.SetInt32("Id", 0);
                session.SetString("Fullname", string.Empty);
                session.SetString("Username", string.Empty);
                session.SetString("Password", string.Empty);
                session.SetString("Email", string.Empty);
                session.SetString("Telephone", string.Empty);
                session.SetString("Address", string.Empty);
                session.SetInt32("Seeker_Id", 0);
                session.SetInt32("Experience", 0);
                session.SetString("Hobby", string.Empty);

                DeleteObject(session, "loggedInJobSeeker");
            }
            CreateJobSeeker(session);
        }
        public static void ResetJobPublisher(this ISession session)
        {
            if (ContainsObject(session, "loggedInUser"))
            {
                session.SetInt32("Id", 0);
                session.SetString("Fullname", string.Empty);
                session.SetString("Username", string.Empty);
                session.SetString("Password", string.Empty);
                session.SetString("Email", string.Empty);
                session.SetString("Telephone", string.Empty);
                session.SetString("Address", string.Empty);
                session.SetInt32("Company_Id", 0);
                session.SetString("Company_Name", string.Empty);
                session.SetString("Company_Address", string.Empty);

                DeleteObject(session, "loggedInUser");
            }
            CreateJobPublisher(session);
        }
        public static void UpdateJobPublisher(this ISession session, CompanyAccountViewModel JobPublisher)
        {
            session.SetInt32("Id", JobPublisher.Id);
            session.SetString("Fullname", JobPublisher.Fullname);
            session.SetString("Username", JobPublisher.Username);
            session.SetString("Password", JobPublisher.Password);
            session.SetString("Email", JobPublisher.Email);
            session.SetString("Telephone", JobPublisher.Telephone);
            session.SetString("Address", JobPublisher.Address);
            session.SetInt32("Company_Id", JobPublisher.Company_Id);
            session.SetString("Company_Name", JobPublisher.Company_Name);
            session.SetString("Company_Address", JobPublisher.Company_Address);
            SetObject(session, "loggedInUser", JobPublisher);
        }
        public static void UpdateJobSeeker(this ISession session, JobSeekerAccountViewModel JobSeeker)
        {
            session.SetInt32("Id", JobSeeker.Id);
            session.SetString("Fullname", JobSeeker.Fullname);
            session.SetString("Username", JobSeeker.Username);
            session.SetString("Password", JobSeeker.Password);
            session.SetString("Email", JobSeeker.Email);
            session.SetString("Telephone", JobSeeker.Telephone);
            session.SetString("Address", JobSeeker.Address);
            session.SetInt32("Seeker_Id", JobSeeker.Seeker_Id);
            session.SetInt32("Experience", JobSeeker.Experience);
            session.SetString("Hobby", JobSeeker.Hobby);
            SetObject(session, "loggedInJobSeeker", JobSeeker);
        }
        public static JobSeekerAccountViewModel GetJobSeeker(this ISession session)
        {
            return GetObject<JobSeekerAccountViewModel>(session, "loggedInJobSeeker");
        }
        public static CompanyAccountViewModel GetJobPublisher(this ISession session)
        {
            return GetObject<CompanyAccountViewModel>(session, "loggedInUser");
        }
        public static void SetObject(this ISession session, string key, object value)
        {
            if (ContainsObject(session, key))
            {
                DeleteObject(session, key);
            }
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static bool ContainsObject(this ISession session, string key)
        {
            return session.Get(key) != null;
        }
        public static void DeleteObject(this ISession session, string key)
        {
            session.Remove(key);
        }
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
