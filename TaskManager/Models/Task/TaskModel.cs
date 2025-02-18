using System;

namespace TaskManager.Models;

public class TaskModel
{
    public int Id { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ModifyDate { get; set; }
    public DateTime DateComplete { get; set; }
}
