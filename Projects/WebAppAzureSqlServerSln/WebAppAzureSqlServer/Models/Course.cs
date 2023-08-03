namespace WebAppAzureSqlServer.Models
{
    public class Course
    {
        // This class is a representation of the structure of our data
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public decimal Rating { get; set; }

    }
}


/* SQL


 create table course
(
 CourseID int,
 CourseName varchar(1000),
 Rating numeric(2,1)
)

insert into course values(1,'AZ-900 Azure Fundamentals',2.0)

insert into course values(2,'AZ-204 Developing Solutions for MS Azure',5.0)
insert into course values(3,'AZ-104 MS Azure Administrator',5.0)

select * from course

update course
set Rating = 4.7
where CourseID = 3 
 
 */