using Tutorial10.Application.Contracts.Request;
using Tutorial10.Application.Contracts.Response;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Mappers.Impl;

public class PrescriptionMapper(
    IPrescriptionMedicamentMapper prescriptionMedicamentMapper,
    IDoctorMapper doctorMapper) : IPrescriptionMapper
{
    public Prescription MapAddPrescriptionDtoToEntity(AddPrescriptionRequestDto prescription)
    {
        return new Prescription
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdDoctor = prescription.DoctorId,
        };
    }

    public PrescriptionResponseDto MapEntityToResponseDto(Prescription prescription)
    {
        var doctor = doctorMapper.MapEntityToResponseDto(prescription.Doctor);
        var prescriptionMedicaments = prescription.PrescriptionMedicaments
                                                  .Select(prescriptionMedicamentMapper.MapEntityToResponseDto)
                                                  .ToList();
        
        return new PrescriptionResponseDto(
            prescription.IdPrescription, 
            prescription.Date, 
            prescription.DueDate, 
            prescriptionMedicaments, 
            doctor);
    }
}