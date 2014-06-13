class Person{	
	constructor(public Age: number){}
	Older(){
		this.Age ++;
	}
}

class Student extends Person{	
	constructor(public Age: number, public StudentNumber: number){
		super(Age);
	}
	Older(){
		this.Age ++;
	}
}

class PersonSpec{
	P: Person;
	S: Student;
	constructor() {	
		
		this.P = new Mock<Person>(Person)
			.With(x => x.Age = 4)
			.Create();
			
		this.S = new Mock<Student>(Student)
			.With(x => x.Age = 5)
			.With(x => x.StudentNumber = 123)
			.Create();
			
	}
}

class Mock<T>{	
	//pass in the 'type' 
	//'type' is actually a function
	private CreatedType: T;
	constructor(private Type: any){}
	
	public With(mutator: (obj:T)=>void){
		//avoid compile time error as type is any. 
		//any can have 0 param constructor
		var t = new this.Type();
		//set reference to newly created type
		this.CreatedType = t;
		//pass it into the passed in function
		mutator(t);
		return this;
	}
	
	public Create():T{
		return this.CreatedType
	}
}