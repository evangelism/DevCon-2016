//tsc main.ts --target ES5

namespace MyCorporation {	
	export class TestClass{
		
	}	
}
	
interface Person {
	firstName: string;
	lastName: string;
}

interface ManagerBase{
	writeEmail();
}

enum Direction {
	Up = 1,
	Down,
	Left,
	Right
}

class Manager implements ManagerBase{
	age: number;
	
	constructor(age: number, private _persons: Person[], 
	public pantsColor : string = 'yellow') {
		this.age = age;
	}
	
	writeEmail() : number{
		this._persons.forEach(c=>{
			console.log(`User: ${c.firstName} ${c.lastName}`);
		});
		
		return this._persons.length;
	}
	
	get grade() {
    	return this.age * 2 / 1.5;
  	}

}

let manager = new Manager(31, [
	<Person>{firstName: 'Ivan', lastName: 'Ivanov'},
	<Person>{firstName: 'Sergey', lastName: 'Pugachev'}
]);