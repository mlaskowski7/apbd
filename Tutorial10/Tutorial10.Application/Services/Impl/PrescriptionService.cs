using Tutorial10.Application.Contracts.Request;
using Tutorial10.Application.Contracts.Response;
using Tutorial10.Application.Mappers;
using Tutorial10.Application.Mappers.Impl;
using Tutorial10.Application.Persistence;
using Tutorial10.Application.Repositories;
using Tutorial10.Application.Utils;
using Tutorial10.Domain.Models;

namespace Tutorial10.Application.Services.Impl;

public class PrescriptionService(
    IUnitOfWork uow,
    IPatientService patientService,
    IMedicamentService medicamentService,
    IDoctorService doctorService,
    IPrescriptionRepository prescriptionRepository,
    IPrescriptionMapper prescriptionMapper) : IPrescriptionService
{
    public async Task<Result<PrescriptionResponseDto>> AddPrescriptionAsync(AddPrescriptionRequestDto addPrescriptionRequestDto,
        CancellationToken cancellationToken = default)
    {
        if (addPrescriptionRequestDto.DueDate < addPrescriptionRequestDto.Date)
        {
            return Result<PrescriptionResponseDto>.Err(Error.BadRequest($"DueDate({addPrescriptionRequestDto.DueDate}) cannot be before Date({addPrescriptionRequestDto.Date})"));
        }
        
        await uow.BeginAsync(cancellationToken);
        var patientRes = await patientService.GetOrCreatePatientAsync(addPrescriptionRequestDto.Patient, cancellationToken);
        if (patientRes.IsErr)
        {
            return Result<PrescriptionResponseDto>.Err(patientRes.Error);
        }
        var patient = patientRes.Value;
        
        var doctorRes = await doctorService.GetDoctorById(addPrescriptionRequestDto.DoctorId, cancellationToken);
        if (doctorRes.IsErr)
        {
            return Result<PrescriptionResponseDto>.Err(doctorRes.Error);
        }
        var doctor = doctorRes.Value;
        
        var prescription = prescriptionMapper.MapAddPrescriptionDtoToEntity(addPrescriptionRequestDto);
        prescription.Doctor = doctor;
        prescription.IdDoctor = doctor.IdDoctor;
        prescription.Patient = patient;
        prescription.IdPatient = patient.IdPatient;
        
        await AddMedicamentsToPrescription(addPrescriptionRequestDto.Medicaments, prescription, cancellationToken);
        
        var (createdPrescription, err) = await prescriptionRepository.CreatePrescriptionAsync(prescription, cancellationToken);
        if (err != null)
        {
            return Result<PrescriptionResponseDto>.Err(err);
        }
        await uow.CommitAsync(cancellationToken);
        
        return Result<PrescriptionResponseDto>.Ok(prescriptionMapper.MapEntityToResponseDto(createdPrescription!));
    }

    private async Task<Error?> AddMedicamentsToPrescription(
        List<PrescriptionMedicamentRequestDto> prescriptionMedicaments, 
        Prescription prescription, 
        CancellationToken cancellationToken = default)
    {
        foreach (var prescriptionMedicamentBody in prescriptionMedicaments)
        {
            var medicamentRes = await medicamentService.GetMedicamentByIdAsync(prescriptionMedicamentBody.IdMedicament, cancellationToken);
            if (medicamentRes.IsErr)
            {
                return medicamentRes.Error;
            }

            var prescriptionMedicament = new PrescriptionMedicament()
            {
                Prescription = prescription,
                Medicament = medicamentRes.Value,
                Dose = prescriptionMedicamentBody.Dose,
                Details = prescriptionMedicamentBody.Description,
            };
            
            prescription.PrescriptionMedicaments.Add(prescriptionMedicament);
        }

        return null;
    }
    
}