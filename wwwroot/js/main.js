let cartButton = document.getElementsByClassName('cartButton');

for(let i = 0; i < cartButton.length; i++){
    cartButton[i].addEventListener('click', function () {
        container[i] = cartButton[i].parentNode;
        container[i].style.border = 'solid 2px #c57d1e';
        //container.classList.remove('addClicked');
    });
}
let productsCount = document.getElementsByClassName('productsCount');
let productsCountNode = [];
for(let i=0;i<productsCount.length;i++){
    productsCountNode[i] = productsCount[i].textContent;
}
let increase = document.getElementsByClassName('increaseProducts');
for(let i=0;i<productsCount.length;i++){
    increase[i].addEventListener('click',function(){
        productsCountNode[i] ++;
        productsCount[i].textContent = productsCountNode[i];
    });
}
let decrease = document.getElementsByClassName('decreaseProducts');
for(let i=0;i<productsCount.length;i++){
    decrease[i].addEventListener('click',function(){
       if(productsCountNode[i]>0){
        productsCountNode[i] --;
        productsCount[i].textContent = productsCountNode[i];
       }
    });
}