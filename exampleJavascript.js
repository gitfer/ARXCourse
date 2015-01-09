//// ==================================================================================
//// Object Literals
//// ==================================================================================

var myLiteralObject = {
	age: 3,
	name: 'TTT',
	properties: ['funny', 'boring', 'committed'],
	doSomething: function(){
		console.log('Object literal, invoking function: doing something...and accessing object\'s name '+ myLiteralObject.name);
	},
	birthdate: new Date(),
	balance: 3.12
};

console.log('Object literal: property access via dot notation or square brakets(for reflection). ', myLiteralObject.age, myLiteralObject['age']);
myLiteralObject.doSomething();
console.log('Object literal: birthdate access', myLiteralObject.birthdate.toLocaleDateString());

//// ==================================================================================
//// Hoisting, Functions, anonimous function, self-invoking functions
//// ==================================================================================

//// dividiArgomenti è funzione globale, invocabile anche prima della dichiarazione a causa dell'hoisting.
//// Hoisting: var statements and function declarations will be moved to the top of their enclosing scope, prima dell'esecuzione vera e propria.
dividiArgomenti();
function dividiArgomenti(argomento) {
	// Se argomento null o undefined stringa vuota
	argomento = argomento || '';
	console.log(' -------------------------------------------------------- ' + argomento);
};

//// Hoisting della variabile divide a cui viene assegnala la funzione anonima.
//// A differenza di dividiArgomenti, non posso invocare divide prima della dichiarazione, perchè si ha hoisiting solo della variabile.

//// Questa invocazione non funziona
//divide();
var divide = function () {
	console.log('--------------------------------------------------------');
};
//// Questa invocazione funziona
//divide();

//// Self invoking functions con passaggio di parametro da fuori
(function (name) {
	console.log('Functions: Hello ' + name + ' from self-invoking function');
})('World');

//// Le funzioni posso restituire a loro volta funzioni
var evaluateDiscount = function (day) {
	var discount50 = function () {
		return 0.5;
	};
	var discount30 = function () {
		return 0.3;
	};

	if(day % 2 === 0){
		return discount50;
	} 
	return discount30; 
}

console.log('Functions: Discount for even days: ', evaluateDiscount(12)() );

console.log('Functions: Discount for odd days: ', evaluateDiscount(3)() );

//// Name resolution
//// var foo = function bar() {
////     bar(); // Works
//// }
//// bar(); // ReferenceError

//// ==================================================================================
//// Function constructors, create instances, augment instance vs types, destroying from instance or from type's prototype, inheritance and destroy from literal
//// ==================================================================================
dividiArgomenti('Function constructors');

//// Usare maiuscola!
var Animal = function(animalName){
    this.name = animalName;
    this.sayHi = function () {
    	console.log('Hi from ' + this.name);
    };
};

//// 2 istanze di animale
var peppa =  new Animal('Peppa');
var george =  new Animal('George');

//// Augmenting instance
peppa.sayHo = function(){
    console.log('Ho from ' + this.name);
};

peppa.sayHo();
//// george can't say Hi
//george.sayHi();

//// Augmenting types
Animal.prototype.emitSound = function(){
    console.log('Grunt from ' + this.name);
};

peppa.emitSound();
george.emitSound();


peppa.sayHo();
delete peppa.sayHo;
//// can't call sayHo anymore
// peppa.sayHo();

delete peppa.constructor.prototype.emitSound;
//// can't call emitSound anymore, both on Peppa and George
// peppa.emitSound();
// george.emitSound();


var transport = {
	numberOfWheels: 2,
	startEngine: function () {
		console.log('Starting engine...')
	}
};

var motorcycle = Object.create(transport);
var car = Object.create(transport);
car.numberOfWheels = 4;
car.activateWipers = function () {
	console.log('Wipers activated');
};

console.log('motorcycle.numberOfWheels', motorcycle.numberOfWheels);
//// 4 ruote
console.log('car.numberOfWheels', car.numberOfWheels);

delete car.numberOfWheels;
console.log('motorcycle.numberOfWheels (after delete) ', motorcycle.numberOfWheels);
//// 2 ruote, risale la prototype chain
console.log('car.numberOfWheels (after delete) ', car.numberOfWheels);

//// Cambio la property su tutte le istanze!!
Object.getPrototypeOf(car).numberOfWheels = 87;
console.log('motorcycle.numberOfWheels', motorcycle.numberOfWheels);
console.log('car.numberOfWheels', car.numberOfWheels);

delete Object.getPrototypeOf(car).numberOfWheels;
// Versione ES6 della riga sopra
// delete car.__proto__.numberOfWheels;
console.log('motorcycle.numberOfWheels', motorcycle.numberOfWheels);
console.log('car.numberOfWheels', car.numberOfWheels);

car.activateWipers();
//// Posso eliminare anche la funzione
delete car.activateWipers;
//// Ora activateWipers non è + invocabile
// car.activateWipers();

motorcycle.startEngine();
car.startEngine();

delete Object.getPrototypeOf(motorcycle).startEngine;
//// Ora startEngine() non è + invocabile ne' su car ne' su motorcycle
// motorcycle.startEngine();
// car.activateWipers();


//// ==================================================================================
//// Closures (usage for encapsulation and timers)
//// ==================================================================================
dividiArgomenti('Closures');

var incrementCount = function (count) {
	return function (second) {
		return count + second;
	};
};
var add = incrementCount(2);
var sumResult = add(3);
console.log('Closures: result is ' + sumResult);


var logAndExecute = function (callbackFunction) {
	console.log('Closures: Logging something...');
	var internalData = 3;
	callbackFunction(internalData);
};

logAndExecute(function (firstOperand) {
	console.log('Callback Function 1st example', firstOperand + 4);
});

logAndExecute(function (firstOperand) {
	console.log('Callback Function 2nd example', firstOperand + 5);
});


//// ==================================================================================
//// Module Revealing Pattern
//// ==================================================================================
dividiArgomenti('MRP');

var myCarModule = (function() {
	var numberOfWheels = 2;
	var _fixedIncrement = 1;

	var myPrivateFunc = function (data) {
		return data*2 + _fixedIncrement;
	};

	var useMyPrivateFunc = function (value) {
		return myPrivateFunc(value);
	};
	return {
		wheels: numberOfWheels,
		calculateConsumeByKms: useMyPrivateFunc
	}
})();

console.log('MRP: calculateConsumeByKms ', myCarModule.calculateConsumeByKms(10)); 


//// ==================================================================================
//// call e apply (this)
//// ==================================================================================
dividiArgomenti('call e apply');

var add = function (firstOperand, secondOperand) {
	this.push(firstOperand);
	this.push(secondOperand);
	console.log(this);
};

add.apply([], [3, 2]);
add.call([], 3, 2);

//// this assume il valore del 1o parametro, essendo ora un oggetto, l'implementazione della funzione rompe perchè non esiste la funzione push sull'object literal
// add.apply({}, [3, 2]);
// add.call({}, 3, 2);

