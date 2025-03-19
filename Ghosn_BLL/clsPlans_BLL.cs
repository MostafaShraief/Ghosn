using Ghosn_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ghosn_BLL
{
    public class PlanDTO
    {
        public int PlanID { get; set; }
        public int ClientID { get; set; }
    }

    public class PlanResponseDTO : PlanDTO
    {
        public OutputResponseDTO? Output { get; set; }
        public InputResponseDTO? Input { get; set; }
    }

    public class PlanSummaryDTO
    {
        public int PlanID { get; set; }
        public bool IsCompleted { get; set; } // Convert IsCompleted to boolean for better readability
        public int? PrizeID { get; set; } // Nullable since it may not always have a value
    }

    public class clsPlans_BLL
    {

        // Retrieve all Plans with related data
        public static List<PlanResponseDTO> GetAllPlansWithDetails()
        {
            var plans = new List<PlanResponseDTO>();

            // Retrieve all Plans from DAL
            var planObjects = clsPlans_DAL.GetAllPlans();
            foreach (var planObject in planObjects)
            {
                var planResponseDTO = new PlanResponseDTO
                {
                    PlanID = planObject.PlanID,
                    ClientID = planObject.ClientID,
                    Output = clsOutputs_BLL.GetOutputWithDetailsById(planObject.OutputID),
                    Input = clsInputs_BLL.GetInputWithPlantsById(planObject.InputID)
                };

                plans.Add(planResponseDTO);
            }

            return plans;
        }

        public static List<PlanSummaryDTO> GetAllPlanSummaries()
        {
            var planObjects = clsPlans_DAL.GetAllSummeryPlans();
            return planObjects.Select(ConvertToSummaryDTO).ToList();
        }

        private static PlanSummaryDTO ConvertToSummaryDTO(PlanObject obj)
        {
            return new PlanSummaryDTO
            {
                PlanID = obj.PlanID,
                IsCompleted = obj.IsCompleted, // Convert integer to boolean
                PrizeID = obj.PrizeID
            };
        }

        // Retrieve a Plan by ID with related data
        public static PlanResponseDTO? GetPlanWithDetailsById(int planID)
        {
            var planObject = clsPlans_DAL.GetPlanById(planID);
            if (planObject == null) return null;

            return new PlanResponseDTO
            {
                Output = clsOutputs_BLL.GetOutputWithDetailsById(planObject.OutputID),
                Input = clsInputs_BLL.GetInputWithPlantsById(planObject.InputID)
            };
        }

        // Add a new Plan with related data
        public static int AddPlanWithDetails(PlanResponseDTO dto)
        {
            if (dto == null || dto.Output == null || dto.Input == null) return 0; // Return failed to add
            
            // Add the Output
            int outputID = clsOutputs_BLL.AddOutputWithDetails(dto.Output);

            // Add the Input
            int inputID = clsInputs_BLL.AddInputWithPlants(dto.Input);

            // Add the Plan
            var planObject = new PlanObject(0, dto.ClientID, inputID, outputID);
            int planID = clsPlans_DAL.AddPlan(planObject);

            if (planID > 0)
            {
                ClientNotificationRequestDTO WelcomeNotification = new ClientNotificationRequestDTO
                {
                    ClientID = dto.ClientID,
                    DateAndTime = DateTime.Now,
                    Title = "خطة جديدة",
                    Body = $"رائع {clsClients_BAL.GetClientById(dto.ClientID).FirstName}! سنقوم بتذكيرك يوميا لمتابعة التقدم، لا تفوت الخطوات القادمة و التذكيرات."
                };
                clsClientNotifications_BLL.AddClientNotification(WelcomeNotification);

                ClientNotificationRequestDTO NextStepNotification = new ClientNotificationRequestDTO
                {
                    ClientID = dto.ClientID,
                    DateAndTime = DateTime.Now,
                    Title = "الخطوة التالية",
                    Body = $"اهلا! تذكير ب{dto.Output.SuggestedTimelines.FirstWeeks[0].Step}."
                };

                clsClientNotifications_BLL.AddClientNotification(NextStepNotification);
            }

            return planID;
        }

        // Update an existing Plan with related data
        public static bool UpdatePlanWithDetails(PlanResponseDTO dto, int planID)
        {
            // Retrieve the existing Plan
            var planObject = clsPlans_DAL.GetPlanById(planID);
            if (dto == null || dto.Output == null || dto.Input == null || planObject == null) return false; // Failed to add

            dto.Output.OutputID = planObject.OutputID;
            dto.Input.InputID = planObject.InputID;
            dto.ClientID = planObject.ClientID;

            // Update the Output
            bool isOutputUpdated = clsOutputs_BLL.UpdateOutputWithDetails(dto.Output);

            // Update the Input
            bool isInputUpdated = clsInputs_BLL.UpdateInputWithPlants(dto.Input);

            // Update the Plan
            var updatedPlanObject = new PlanObject(planID, dto.ClientID, planObject.InputID, planObject.OutputID);
            bool isPlanUpdated = clsPlans_DAL.UpdatePlan(updatedPlanObject);

            return isOutputUpdated && isInputUpdated && isPlanUpdated;
        }

        /// <summary>
        /// Update the winner (PrizeID) for a specific plan.
        /// </summary>
        /// <param name="planID">The ID of the plan.</param>
        /// <param name="prizeID">The ID of the prize.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        public static bool UpdatePlanWinner(int planID, int prizeID)
        {
            bool IsUpdated = clsPlans_DAL.UpdatePlanWinner(planID, prizeID);

            if (IsUpdated)
            {
                ClientNotificationRequestDTO WinNotification = new ClientNotificationRequestDTO
                {
                    ClientID = clsPlans_DAL.GetPlanById(planID).ClientID,
                    DateAndTime = DateTime.Now,
                    Title = "خطة رابحة",
                    Body = $"مبارك! نالت خطتك المركز الأول في إحدى المسابقات، كتقدير لجهودك نود إهدائك جائزة قيمتها {clsPrizes_DAL.GetPrizeById(prizeID).PrizeMoney} دولار."
                };
                clsClientNotifications_BLL.AddClientNotification(WinNotification);
            }

            return IsUpdated;
        }

        /// <summary>
        /// Mark a plan as completed.
        /// </summary>
        /// <param name="planID">The ID of the plan.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        public static bool UpdatePlanCompleted(int planID)
        {
            bool IsUpdated = clsPlans_DAL.UpdatePlanCompleted(planID);

            if (IsUpdated)
            {
                ClientNotificationRequestDTO WinNotification = new ClientNotificationRequestDTO
                {
                    ClientID = clsPlans_DAL.GetPlanById(planID).ClientID,
                    DateAndTime = DateTime.Now,
                    Title = "خطة مكتملة",
                    Body = $"تهانيا على إكمال الخطة و المثابرة و الصبر على تحقيق غايتكم، شكرا لكم."
                };
                clsClientNotifications_BLL.AddClientNotification(WinNotification);
            }

            return IsUpdated;
        }

        // Delete a Plan and its related data
        public static bool DeletePlanWithDetails(int planID)
        {
            // Retrieve the Plan
            var planObject = clsPlans_DAL.GetPlanById(planID);
            if (planObject == null) return false;

            // Delete the Output and its related data
            bool isOutputDeleted = clsOutputs_BLL.DeleteOutputWithDetails(planObject.OutputID);

            // Delete the Input and its related data
            bool isInputDeleted = clsInputs_BLL.DeleteInputWithPlants(planObject.InputID);

            // Delete the Plan
            bool isPlanDeleted = clsPlans_DAL.DeletePlan(planID);

            return isOutputDeleted && isInputDeleted && clsOutputs_DAL.DeleteOutput(planObject.OutputID) && 
                clsInputs_DAL.DeleteInput(planObject.InputID) && isPlanDeleted;
        }

        // Delete all Plans and their related data
        //public static bool DeleteAllPlans()
        //{
        //    // Retrieve all Plans
        //    var planObjects = clsPlans_DAL.GetAllPlans();

        //    // Delete each Plan and its related data
        //    foreach (var planObject in planObjects)
        //    {
        //        DeletePlanWithDetails(planObject.PlanID);
        //    }

        //    return true;
        //}

        public static List<PlanResponseDTO> GetPlansWithDetailsByClientId(int clientId)
        {
            var plans = new List<PlanResponseDTO>();

            // Retrieve plans by ClientID from DAL
            var planObjects = clsPlans_DAL.GetPlansByClientId(clientId);
            foreach (var planObject in planObjects)
            {
                var planResponseDTO = new PlanResponseDTO
                {
                    PlanID = planObject.PlanID,
                    ClientID = planObject.ClientID,
                    Output = clsOutputs_BLL.GetOutputWithDetailsById(planObject.OutputID),
                    Input = clsInputs_BLL.GetInputWithPlantsById(planObject.InputID)
                };

                plans.Add(planResponseDTO);
            }

            return plans;
        }

        //public static bool UpdatePlanWithDetailsByClientId(int clientId, PlanRequestDTO dto)
        //{
        //    if (dto == null || dto.Output == null || dto.Input == null) return false;

        //    // Retrieve the existing plans for the client
        //    var planObjects = clsPlans_DAL.GetPlansByClientId(clientId);
        //    if (planObjects == null || !planObjects.Any()) return false;

        //    // Update each plan for the client
        //    foreach (var planObject in planObjects)
        //    {
        //        // Update the Output
        //        bool isOutputUpdated = clsOutputs_BLL.UpdateOutputWithDetails(dto.Output);

        //        // Update the Input
        //        bool isInputUpdated = clsInputs_BLL.UpdateInputWithPlants(dto.Input);

        //        // Update the Plan
        //        var updatedPlanObject = new PlanObject(planObject.PlanID, clientId, planObject.OutputID, planObject.InputID);
        //        bool isPlanUpdated = clsPlans_DAL.UpdatePlan(updatedPlanObject);

        //        if (!isOutputUpdated || !isInputUpdated || !isPlanUpdated)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        //public static bool DeletePlansWithDetailsByClientId(int clientId)
        //{
        //    // Retrieve the plans for the client
        //    var planObjects = clsPlans_DAL.GetPlansByClientId(clientId);
        //    if (planObjects == null || !planObjects.Any()) return false;

        //    // Delete each plan and its related data
        //    foreach (var planObject in planObjects)
        //    {
        //        // Delete the Output and its related data
        //        bool isOutputDeleted = clsOutputs_BLL.DeleteOutputWithDetails(planObject.OutputID);

        //        // Delete the Input and its related data
        //        bool isInputDeleted = clsInputs_BLL.DeleteInputWithPlants(planObject.InputID);

        //        // Delete the Plan
        //        bool isPlanDeleted = clsPlans_DAL.DeletePlan(planObject.PlanID);

        //        if (!isOutputDeleted || !isInputDeleted || !isPlanDeleted)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        // Conversion method: DAL Object to DTO
        private static PlanDTO ConvertToDTO(PlanObject obj)
        {
            return new PlanDTO
            {
                PlanID = obj.PlanID,
                ClientID = obj.ClientID
            };
        }

        // Conversion method: DTO to DAL Object
        private static PlanObject ConvertToDALObject(PlanDTO dto)
        {
            return new PlanObject(dto.PlanID, dto.ClientID, 0, 0); // OutputID and InputID are set to 0 as they are managed separately
        }
    }
}