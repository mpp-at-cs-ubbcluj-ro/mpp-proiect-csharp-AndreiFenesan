
using System;
using System.Collections.Generic;
using lab5.models;
using lab5.dtos;
namespace lab5.servicies;

public interface IService
{
   Boolean AuthenticateVolunteer(String username, String password);
   Boolean AddNewDonation(long charityCaseId, String name, String emailAddress, String phoneNumber, double donationAmount);
   IEnumerator<Donor> GetDonorsWithNameContaining(String containsInName);
   List<CharityCaseDto> GetAllCharityCases();
}