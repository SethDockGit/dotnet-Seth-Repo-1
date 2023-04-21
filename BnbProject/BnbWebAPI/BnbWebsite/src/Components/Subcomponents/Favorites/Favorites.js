import ListingsCard from "../ListingsCard/ListingsCard";

export default function Favorites({
    favorites,
    listings
}){
    
    var userFavorites = favorites.map(function(val) {

        return(
            listings.find(l => l.id == val)
        )
    })

    const jsx = userFavorites.map(function(val, index) {

        return(
            <div key={index}>
                <ListingsCard listing={val}/>
            </div>
        )
    });

    return jsx;
}
