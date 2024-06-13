const Capitalize = (word: string) => {
    return word.charAt(0).toUpperCase() + word.slice(1);
};

const Uncapitalzie = (word: string) => {
    return word.charAt(0).toLowerCase() + word.slice(1);
}

const StringUtils = { Capitalize, Uncapitalzie };

export default StringUtils;