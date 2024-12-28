namespace MediStoS.Enums;

public enum Roles
{
    WarehouseWorker = 0,// Ordinary warehouse worker, can view medicine and batches and storage violations
    Manager = 1, // Manager, has all previous permissions of a worker, but can also add, edit and delete medicine, batches and sensors
    Admin = 2, // have all permissions, including adding, editing and deleting warehouses and storage violations. Also can edit user info and delete users.
    DBAdmin = 3 //Can manipulate the db including making backups and migrations
}
