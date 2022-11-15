use master
Drop database OnlineExamSystem
Create database OnlineExamSystem
use OnlineExamSystem
CREATE TABLE ACCOUNT(
	[ACCOUNT_ID] INT IDENTITY (1,1) PRIMARY KEY,
	[USERNAME] VARCHAR(255),
	[PASSWORD] VARCHAR(20),
	[ROLE] VARCHAR(20),
	[EMAIL] VARCHAR(255),
)


CREATE TABLE [USER](
	[USER_ID] INT PRIMARY KEY,
	[NAME] VARCHAR(255),
	[ROLE] VARCHAR(50),
	[IMAGE] VARCHAR(MAX),
	[PHONE] VARCHAR(10),
	[ADDRESS] VARCHAR(255),
	[GENDER] VARCHAR(20),
	[EMAIL] VARCHAR(255),
	FOREIGN KEY([USER_ID])
	REFERENCES ACCOUNT([ACCOUNT_ID])
)
CREATE Table Category(
CATEGORY_ID INT IDENTITY (1,1) PRIMARY KEY,
CATEGORY_NAME VARCHAR(255) UNIQUE,

)

CREATE TABLE EXAM(
	[EXAM_ID] INT IDENTITY (1,1) PRIMARY KEY,
	[OWNER] INT,
	[EXAM_NAME] VARCHAR(255),
	[TIME] INT,
	CATEGORY_ID INT, 
	[NUMOFQUESTION] INT,
	[PICTURES] VARCHAR(MAX),
	[CREATED_DATE] DATETIME DEFAULT GETDATE(),
	FOREIGN KEY([OWNER])
	REFERENCES ACCOUNT([ACCOUNT_ID]),
	FOREIGN KEY(CATEGORY_ID)
	REFERENCES Category(CATEGORY_ID)
)

CREATE TABLE QUESTION(
	[QUESTION_ID] INT IDENTITY (1,1) PRIMARY KEY,
	[QUESTION] VARCHAR(MAX),
	[ANSWER1] VARCHAR(MAX),
	[ANSWER2] VARCHAR(MAX),
	[ANSWER3] VARCHAR(MAX),
	[ANSWER4] VARCHAR(MAX),
	[CORRECT_ANSWER] INT,
	[EXAM_ID] INT,
	FOREIGN KEY([EXAM_ID])
	REFERENCES EXAM([EXAM_ID])
)
CREATE TABLE COMPLETED_EXAM(
	[COMPLETED_EXAM_ID] INT IDENTITY (1,1) PRIMARY KEY,
	[EXAM_ID] INT,
	[USER_ID] INT,
	[MARK] FLOAT,
	[CREATED_DATE] DATETIME DEFAULT GETDATE(),
	FOREIGN KEY([USER_ID])
	REFERENCES [USER]([USER_ID]),
	FOREIGN KEY([EXAM_ID])
	REFERENCES EXAM([EXAM_ID])
)
CREATE TABLE EXAM_ANSWER(
	[EXAM_ANSWER_ID] INT IDENTITY (1,1) PRIMARY KEY,
	[COMPLETED_EXAM_ID] INT,
	[QUESTION_ID] INT,
	[ANSWER] INT,
	[ANSWER_CONTENT] VARCHAR(MAX),
	[CORRECT_ANSWER] int,
	FOREIGN KEY([COMPLETED_EXAM_ID])
	REFERENCES COMPLETED_EXAM([COMPLETED_EXAM_ID]),
	FOREIGN KEY([QUESTION_ID])
	REFERENCES QUESTION([QUESTION_ID])
)

Insert into ACCOUNT(USERNAME,[PASSWORD],EMAIL,[ROLE])  values('teacher1',123456,'a@email.com','Teacher')
Insert into [USER](USER_ID,NAME,ROLE,GENDER,EMAIL) values(1,'Teacher1','Teacher','Male','a@email.com')
Insert into ACCOUNT(USERNAME,[PASSWORD],EMAIL,[ROLE])  values('teacher2',123456,'a@email.com','Teacher')
Insert into [USER](USER_ID,NAME,ROLE,GENDER,EMAIL) values(2,'Teacher2','Teacher','Male','a@email.com')
Insert into ACCOUNT(USERNAME,[PASSWORD],EMAIL,[ROLE])  values('student1',123456,'a1@email.com','Student')
Insert into [USER](USER_ID,NAME,ROLE,GENDER,EMAIL) values(3,'HocSinh1','Student','Male','a1@email.com')
Insert into ACCOUNT(USERNAME,[PASSWORD],EMAIL,[ROLE])  values('student2',123456,'a2@email.com','Student')
Insert into [USER](USER_ID,NAME,ROLE,GENDER,EMAIL) values(4,'HocSinh2','Student','Male','a2@email.com')


Insert into Category(CATEGORY_NAME) values('MATH')
Insert into Category(CATEGORY_NAME) values('CODE')
Insert into Category(CATEGORY_NAME) values('ENGLISH')
Insert into EXAM(OWNER,EXAM_NAME,TIME,CATEGORY_ID,NUMOFQUESTION,PICTURES) values(1,'Java Core',1,2,10,'img/courses-2.jpg')
Insert into EXAM(OWNER,EXAM_NAME,TIME,CATEGORY_ID,NUMOFQUESTION,PICTURES) values(1,'Math',10,1,10,'img/courses-2.jpg')
Insert into EXAM(OWNER,EXAM_NAME,TIME,CATEGORY_ID,NUMOFQUESTION,PICTURES) values(2,'English',10,3,10,'img/courses-2.jpg')
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('What type of computing technology refers to services and applications that typically run on a distributed network through virtualized resources?','Distributed Computing','Cloud Computing','Soft Computing','Parallel Computing',2,1)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Which one of the following options can be considered as the Cloud?','Hadoop','Intranet','Web Applications','All of the mentioned',1,1)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Cloud computing is a kind of abstraction which is based on the notion of combining physical resources and represents them as _______resources to users','Real','Cloud','Virtual','none of the mentioned',3,1)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Which of the following has many features of that is now known as cloud computing?','Web Service','Softwares','All of the mentioned','Internet',4,1)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Which one of the following cloud concepts is related to sharing and pooling the resources?','Polymorphism','Virtualization','Abstraction','None of the mentioned',2,1)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Which one of the following statements is not true?','The popularization of the Internet actually enabled most cloud computing systems','Cloud computing makes the long-held dream of utility as a payment possible for you, with an infinitely scalable, universally available system, pay what you use','Soft computing addresses a real paradigm in the way in which the system is deployed','All of the mentioned',3,1)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Which one of the following can be considered as a utility is a dream that dates from the beginning of the computing industry itself','Computing','Model','Software','All of the mentioned',1,1)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Which of the following is an essential concept related to Cloud?','Reliability','Abstraction','Productivity','All of the mentioned',2,1)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Which one of the following is Cloud Platform by Amazon?','Azure','AWS','Cloudera','All of the mentioned',2,1)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Which of the following statement is not true?','Through cloud computing, one can begin with very small and become big in a rapid manner.','All applications benefit from deployment in the Cloud.','Cloud computing is revolutionary, even though the technology it is built on is evolutionary','None of the mentioned',2,1)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Two numbers have a difference of 0.7 and a sum of 1. What are the numbers?','0.85 and 0.25','0.95 and 0.15','0.25 and 0.95','0.85 and 0.15',2,2)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('What is 25% of 1400?','700','350','1050','1000',2,2)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Using the long division method, calculate 3598 ÷ 14.','297','282','257','242',2,2)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('In a class of 30 pupils, the ratio of girls to boys is 4:1. How many boys are in the class?','6','24','8','22',2,2)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('In the number 248 803.42, in what places are the digits 2?','ten, thousands and tenths','thousands and hundredths','hundred, thousands and hundredths','hundred, thousands and tenths',2,2)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Calculate: 45 × 19','855','900','810','825',1,2)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('What is the number 542 in Roman numerals','DXXXII','CDXLII	','CCLXII','DXLII',2,2)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('What shape has 2 flat faces, 1 curved face, 2 edges and 0 vertices','cone','sphere','cube','cylinder',3,2)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('Calculate: 9 × (5 + 6) + 4','135','103','55','47',1,2)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('How would you express 1.54m in cm','1540cm','15.4cm','154cm','0.154cm',4,2)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('The simple definition of a noun is: a person, place or','pronoun','thing','function','first letter',1,3)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('A word is almost certainly a noun if it ends with','ness','est','govern','government',2,3)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('In which sentence does a noun follow a determiner','Their team played well','adjectives','adverbs','an adjective',4,3)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('In which sentence is the subject a pronoun','England is cold ','subject of','an adjective','an adjective',2,3)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('A word is almost certainly a noun if it ends with','ness','est','govern','government',2,3)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('A word is almost certainly a noun if it ends with','ness','est','govern','government',2,3)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('A word is almost certainly a noun if it ends with','ness','est','govern','government',2,3)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('A word is almost certainly a noun if it ends with','ness','est','govern','government',2,3)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('A word is almost certainly a noun if it ends with','ness','est','govern','government',2,3)
Insert into QUESTION(QUESTION,ANSWER1,ANSWER2,ANSWER3,ANSWER4,CORRECT_ANSWER,EXAM_ID)values('A word is almost certainly a noun if it ends with','ness','est','govern','government',2,3)
