export default class utils {

    static async wait(delay: number): Promise<void> {
        return new Promise((resolve) => setTimeout(resolve, delay));
    }

    static isDevelopment(): boolean {
        return import.meta.env.MODE === 'development';
    }

    static capitalizeText(text: string): string {
        return text && text.length >= 1 ? text.charAt(0).toUpperCase() + text.slice(1) : text;
    }

    static randomNumber(min:number, max:number): number {
        return Math.floor(min + Math.random() * (max - min + 1));
    }
}